﻿ALTER TABLE [dbo].[QUESTOES]
    ADD CONSTRAINT [DF_QUESTOES_EXCLUIDO] DEFAULT ((0)) FOR [EXCLUIDO];

