﻿ALTER TABLE [dbo].[CBO_OCUPACOES]
    ADD CONSTRAINT [DF_CBO_OCUPACOES_USUARIO_INCLUSAO] DEFAULT (N'system') FOR [USUARIO_INCLUSAO];

