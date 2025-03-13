using System.Threading.Tasks;

namespace GoFoodyProject.Utility
{
    public class EmailService
    {
        private readonly IEmailSender _emailSender;

        public EmailService(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task SendRegistrationEmailAsync(string email, string fullName, string loginUrl)
        {
            string emailContent = $@"
            <html>
            <head>
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                        background-color: #f4f4f4;
                        padding: 20px;
                        text-align: center;
                    }}
                    .container {{
                        background-color: #ffffff;
                        padding: 20px;
                        border-radius: 10px;
                        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                        max-width: 500px;
                        margin: auto;
                    }}
                    h1 {{
                        color: #333;
                    }}
                    p {{
                        color: #555;
                        font-size: 16px;
                    }}
                    .btn {{
                        display: inline-block;
                        padding: 10px 20px;
                        margin-top: 10px;
                        background-color: #007bff;
                        color: #fff;
                        text-decoration: none;
                        border-radius: 5px;
                    }}
                    .btn:hover {{
                        background-color: #0056b3;
                    }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <h1>🎉 Welcome to GoFoody!</h1>
                    <p>Dear {fullName},</p>
                    <p>Thank you for registering with us. Your account has been successfully created.</p>
                    <p>Click the button below to log in and start exploring:</p>
                    <a href='{loginUrl}' class='btn'>Go to Login</a>
                    <p>If you did not register for an account, please ignore this email.</p>
                    <p>Best regards,</p>
                    <p><strong>GoFoody Team</strong></p>
                </div>
            </body>
            </html>";

            await _emailSender.SendEmailAsync(email, "Welcome to GoFoody!", emailContent);
        }
    }
}
