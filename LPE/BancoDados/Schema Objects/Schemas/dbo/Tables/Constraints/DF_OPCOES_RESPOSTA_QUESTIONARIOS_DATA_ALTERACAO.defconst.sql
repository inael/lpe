﻿ALTER TABLE [dbo].[OPCOES_RESPOSTA_QUESTIONARIOS]
    ADD CONSTRAINT [DF_OPCOES_RESPOSTA_QUESTIONARIOS_DATA_ALTERACAO] DEFAULT (getdate()) FOR [DATA_ALTERACAO];

