﻿ALTER TABLE [dbo].[CBO_FAMILIAS]
    ADD CONSTRAINT [DF_CBO_FAMILIA_USUARIO_ALTERACAO] DEFAULT (N'system') FOR [USUARIO_ALTERACAO];

