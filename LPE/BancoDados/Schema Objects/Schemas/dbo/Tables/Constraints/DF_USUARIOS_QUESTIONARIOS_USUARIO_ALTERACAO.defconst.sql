﻿ALTER TABLE [dbo].[USUARIOS_QUESTIONARIOS]
    ADD CONSTRAINT [DF_USUARIOS_QUESTIONARIOS_USUARIO_ALTERACAO] DEFAULT (N'System') FOR [USUARIO_ALTERACAO];

