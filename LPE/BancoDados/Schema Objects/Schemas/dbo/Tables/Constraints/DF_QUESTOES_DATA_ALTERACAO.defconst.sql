﻿ALTER TABLE [dbo].[QUESTOES]
    ADD CONSTRAINT [DF_QUESTOES_DATA_ALTERACAO] DEFAULT (getdate()) FOR [DATA_ALTERACAO];

