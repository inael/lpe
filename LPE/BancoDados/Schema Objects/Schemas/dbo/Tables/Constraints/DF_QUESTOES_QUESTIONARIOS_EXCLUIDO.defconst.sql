﻿ALTER TABLE [dbo].[QUESTOES_QUESTIONARIOS]
    ADD CONSTRAINT [DF_QUESTOES_QUESTIONARIOS_EXCLUIDO] DEFAULT ((0)) FOR [EXCLUIDO];
