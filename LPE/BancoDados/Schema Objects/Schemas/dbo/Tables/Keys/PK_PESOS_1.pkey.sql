﻿ALTER TABLE [dbo].[OPCOES_RESPOSTA]
    ADD CONSTRAINT [PK_PESOS] PRIMARY KEY CLUSTERED ([ID_OPCAO_RESPOSTA] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

