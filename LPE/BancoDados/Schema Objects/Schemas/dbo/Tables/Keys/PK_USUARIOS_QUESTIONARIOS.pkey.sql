﻿ALTER TABLE [dbo].[USUARIOS_QUESTIONARIOS]
    ADD CONSTRAINT [PK_USUARIOS_QUESTIONARIOS] PRIMARY KEY CLUSTERED ([ID_USUARIO_QUESTIONARIO] ASC, [ID_USUARIO] ASC, [ID_QUESTIONARIO] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


