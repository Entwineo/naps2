﻿using System.Threading;
using MimeKit;

namespace NAPS2.ImportExport.Email.Oauth;

public class GmailEmailProvider : MimeEmailProvider
{
    private readonly GmailOauthProvider _gmailOauthProvider;

    public GmailEmailProvider(GmailOauthProvider gmailOauthProvider)
    {
        _gmailOauthProvider = gmailOauthProvider;
    }
        
    protected override async Task SendMimeMessage(MimeMessage message, ProgressHandler progressCallback, CancellationToken cancelToken)
    {
        var messageId = await _gmailOauthProvider.UploadDraft(message.ToString(), progressCallback, cancelToken);
        var userEmail = _gmailOauthProvider.User;
        // Open the draft in the user's browser
        Process.Start($"https://mail.google.com/mail/?authuser={userEmail}#drafts?compose={messageId}");
    }
}