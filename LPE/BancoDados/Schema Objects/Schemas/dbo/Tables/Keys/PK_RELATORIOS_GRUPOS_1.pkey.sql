﻿ALTER TABLE [dbo].[RELATORIOS_GRUPOS]
    ADD CONSTRAINT [PK_RELATORIOS_GRUPOS] PRIMARY KEY CLUSTERED ([ID_GRUPO] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
