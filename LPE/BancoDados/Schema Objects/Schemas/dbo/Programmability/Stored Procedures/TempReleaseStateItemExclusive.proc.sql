﻿
        CREATE PROCEDURE dbo.TempReleaseStateItemExclusive
            @id         tSessionId,
            @lockCookie int
        AS
            UPDATE [DB_39193_lpe].dbo.ASPStateTempSessions
            SET Expires = DATEADD(n, Timeout, GETUTCDATE()), 
                Locked = 0
            WHERE SessionId = @id AND LockCookie = @lockCookie

            RETURN 0