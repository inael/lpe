﻿ALTER TABLE [dbo].[CBO_SINONIMOS]
    ADD CONSTRAINT [FK_CBO_SINONIMOS_CBO_OCUPACOES] FOREIGN KEY ([ID_OCUPACAO]) REFERENCES [dbo].[CBO_OCUPACOES] ([ID_OCUPACAO]) ON DELETE NO ACTION ON UPDATE NO ACTION;

