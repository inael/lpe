﻿ALTER TABLE [dbo].[RELATORIOS]
    ADD CONSTRAINT [DF_RELATORIOS_USUARIO_ALTERACAO] DEFAULT (N'System') FOR [USUARIO_ALTERACAO];

