﻿ALTER TABLE [dbo].[OPCOES_RESPOSTA_QUESTIONARIOS]
    ADD CONSTRAINT [FK_OPCAO_RESPOSTA_QUESTIONARIO_QUESTIONARIOS] FOREIGN KEY ([ID_QUESTIONARIO]) REFERENCES [dbo].[QUESTIONARIOS] ([ID_QUESTIONARIO]) ON DELETE NO ACTION ON UPDATE NO ACTION;
