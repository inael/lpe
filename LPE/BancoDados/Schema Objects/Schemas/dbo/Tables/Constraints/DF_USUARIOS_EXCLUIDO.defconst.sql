﻿ALTER TABLE [dbo].[USUARIOS]
    ADD CONSTRAINT [DF_USUARIOS_EXCLUIDO] DEFAULT ((0)) FOR [EXCLUIDO];

