
        CREATE PROCEDURE dbo.DeleteExpiredSessions
        AS
            DECLARE @now datetime
            SET @now = GETUTCDATE()

            DELETE [DB_39193_lpe].dbo.ASPStateTempSessions
            WHERE Expires < @now

            RETURN 0