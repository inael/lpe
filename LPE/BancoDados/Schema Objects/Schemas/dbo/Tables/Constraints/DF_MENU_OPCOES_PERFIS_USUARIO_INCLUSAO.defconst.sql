﻿ALTER TABLE [dbo].[MENU_OPCOES_PERFIS]
    ADD CONSTRAINT [DF_MENU_OPCOES_PERFIS_USUARIO_INCLUSAO] DEFAULT (N'system') FOR [USUARIO_INCLUSAO];

