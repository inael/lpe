﻿ALTER TABLE [dbo].[QUESTIONARIOS]
    ADD CONSTRAINT [DF_QUESTIONARIOS_EXCLUIDO] DEFAULT ((0)) FOR [EXCLUIDO];
