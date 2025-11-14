# Migration Notes for Users from v1 to v2

## Breaking Changes

- Attachment[] → MimeEntity[] parameters
- MailMessage → MimeMessage parameters
- Async methods now have optional CancellationToken parameters

## Example Migration

Before (System.Net.Mail):

```csharp
using System.Net.Mail;

var attachment = new Attachment("file.pdf");

sender.Send("from@example.com", "to@example.com", "Subject", "Body", null, attachment);
```

After (MailKit):

```csharp
using MimeKit;

var attachment = new MimePart("application", "pdf") {
	Content = new MimeContent(File.OpenRead("file.pdf")),
	FileName = "file.pdf"
};

sender.Send("from@example.com", "to@example.com", "Subject", "Body", null, attachment);
```