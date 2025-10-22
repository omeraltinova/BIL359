using System.Data;
using MySqlConnector;
using Week3.Web.Services;

namespace Week3.Web.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this WebApplication app)
        {
            app.MapPost("/api/auth/register", async (IConfiguration config, RegisterRequest req) =>
            {
                if (string.IsNullOrWhiteSpace(req.Name) || string.IsNullOrWhiteSpace(req.Email) || string.IsNullOrWhiteSpace(req.Password))
                {
                    return Results.BadRequest(new { message = "Eksik bilgi" });
                }

                var connStr = config.GetConnectionString("DefaultConnection");
                await using var conn = new MySqlConnection(connStr);
                await conn.OpenAsync();

                // Check if email exists
                await using (var checkCmd = new MySqlCommand("SELECT id FROM users WHERE email = @e LIMIT 1", conn))
                {
                    checkCmd.Parameters.AddWithValue("@e", req.Email);
                    var exists = await checkCmd.ExecuteScalarAsync();
                    if (exists != null)
                    {
                        return Results.Conflict(new { message = "Bu e-posta zaten kayıtlı" });
                    }
                }

                var hash = PasswordHasher.HashPassword(req.Password);

                await using var tx = await conn.BeginTransactionAsync();
                try
                {
                    long userId;
                    await using (var insertUser = new MySqlCommand(
                        "INSERT INTO users(name, email, password_hash) VALUES(@n, @e, @p); SELECT LAST_INSERT_ID();",
                        conn, (MySqlTransaction)tx))
                    {
                        insertUser.Parameters.AddWithValue("@n", req.Name);
                        insertUser.Parameters.AddWithValue("@e", req.Email);
                        insertUser.Parameters.AddWithValue("@p", hash);
                        var idObj = await insertUser.ExecuteScalarAsync();
                        userId = Convert.ToInt64(idObj);
                    }

                    // Assign default role 'User'
                    await using (var roleCmd = new MySqlCommand(
                        "INSERT INTO user_roles(user_id, role_id) SELECT @uid, id FROM roles WHERE name = 'User' LIMIT 1;",
                        conn, (MySqlTransaction)tx))
                    {
                        roleCmd.Parameters.AddWithValue("@uid", userId);
                        await roleCmd.ExecuteNonQueryAsync();
                    }

                    await tx.CommitAsync();
                    return Results.Ok(new { message = "Kayıt başarılı" });
                }
                catch
                {
                    await tx.RollbackAsync();
                    throw;
                }
            });

            app.MapPost("/api/auth/login", async (IConfiguration config, LoginRequest req) =>
            {
                if (string.IsNullOrWhiteSpace(req.Email) || string.IsNullOrWhiteSpace(req.Password))
                {
                    return Results.BadRequest(new { message = "Eksik bilgi" });
                }

                var connStr = config.GetConnectionString("DefaultConnection");
                await using var conn = new MySqlConnection(connStr);
                await conn.OpenAsync();

                await using var cmd = new MySqlCommand("SELECT id, password_hash FROM users WHERE email = @e LIMIT 1", conn);
                cmd.Parameters.AddWithValue("@e", req.Email);
                await using var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow);
                if (!await reader.ReadAsync())
                {
                    return Results.Unauthorized();
                }
                var hash = reader.GetString("password_hash");
                var ok = PasswordHasher.VerifyPassword(req.Password, hash);
                if (!ok)
                {
                    return Results.Unauthorized();
                }
                return Results.Ok(new { message = "Giriş başarılı" });
            });
        }
    }

    public record RegisterRequest(string Name, string Email, string Password);
    public record LoginRequest(string Email, string Password);
}


