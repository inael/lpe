﻿ALTER TABLE [dbo].[CBO_GRUPOS]
    ADD CONSTRAINT [DF_CBO_GRUPO_EXCLUIDO] DEFAULT ((0)) FOR [EXCLUIDO];
