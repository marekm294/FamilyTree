# Emails

SmtpServer is used to send emails. Inject IEmailSender and call it's only method.

### Local development
To send an emil in local (or in tests) you must set correct values in user secrets.

#### MailHog
Mailhog is good option for local email sending.
https://github.com/mailhog/MailHog. You can simply download and run mailhog.exe.
Mailhog has default port 1025 so you can simply enter this to UserSecrets.
```
"Emails": {
  "SmtpServer": {
    "Host": "localhost",
    "Port": "1025",
    "Username": "",
    "Password": ""
}
```
To see sent emails you can simply type [http://localhost:8025](http://localhost:8025) 
to the browser (when the mailhog is running).

