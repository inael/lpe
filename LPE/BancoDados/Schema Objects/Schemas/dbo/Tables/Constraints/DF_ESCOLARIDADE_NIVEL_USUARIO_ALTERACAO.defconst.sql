﻿ALTER TABLE [dbo].[ESCOLARIDADE_NIVEL]
    ADD CONSTRAINT [DF_ESCOLARIDADE_NIVEL_USUARIO_ALTERACAO] DEFAULT (N'system') FOR [USUARIO_ALTERACAO];

