﻿ALTER TABLE [dbo].[MENU_OPCOES_PERFIS]
    ADD CONSTRAINT [DF_MENU_OPCAO_PERFIL_EXCLUIDO] DEFAULT ((0)) FOR [EXCLUIDO];

