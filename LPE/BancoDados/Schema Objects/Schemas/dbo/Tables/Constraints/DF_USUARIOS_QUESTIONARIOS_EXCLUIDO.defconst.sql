﻿ALTER TABLE [dbo].[USUARIOS_QUESTIONARIOS]
    ADD CONSTRAINT [DF_USUARIOS_QUESTIONARIOS_EXCLUIDO] DEFAULT ((0)) FOR [EXCLUIDO];
