﻿ALTER TABLE [dbo].[MUNICIPIOS]
    ADD CONSTRAINT [DF_MUNICIPIOS_USUARIO_ALTERACAO] DEFAULT (N'system') FOR [USUARIO_ALTERACAO];

