﻿ALTER TABLE [dbo].[ESCOLARIDADE]
    ADD CONSTRAINT [DF_ESCOLARIDADE_USUARIO_INCLUSAO] DEFAULT (N'System') FOR [USUARIO_INCLUSAO];
