﻿ALTER TABLE [dbo].[QUESTOES]
    ADD CONSTRAINT [DF_QUESTOES_DATA_INCLUSAO] DEFAULT (getdate()) FOR [DATA_INCLUSAO];
