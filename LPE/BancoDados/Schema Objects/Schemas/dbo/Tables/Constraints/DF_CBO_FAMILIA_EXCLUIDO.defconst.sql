﻿ALTER TABLE [dbo].[CBO_FAMILIAS]
    ADD CONSTRAINT [DF_CBO_FAMILIA_EXCLUIDO] DEFAULT ((0)) FOR [EXCLUIDO];
