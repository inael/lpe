﻿ALTER TABLE [dbo].[RESULTADOS]
    ADD CONSTRAINT [FK_RESULTADOS_RELATORIOS] FOREIGN KEY ([ID_RELATORIO]) REFERENCES [dbo].[RELATORIOS] ([ID_RELATORIO]) ON DELETE NO ACTION ON UPDATE NO ACTION;

