﻿ALTER TABLE [dbo].[USUARIOS_QUESTIONARIOS]
    ADD CONSTRAINT [FK_USUARIOS_QUESTIONARIOS_QUESTIONARIOS] FOREIGN KEY ([ID_QUESTIONARIO]) REFERENCES [dbo].[QUESTIONARIOS] ([ID_QUESTIONARIO]) ON DELETE NO ACTION ON UPDATE NO ACTION;
