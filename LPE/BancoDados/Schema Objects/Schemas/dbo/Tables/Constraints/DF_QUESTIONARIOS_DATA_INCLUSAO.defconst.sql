﻿ALTER TABLE [dbo].[QUESTIONARIOS]
    ADD CONSTRAINT [DF_QUESTIONARIOS_DATA_INCLUSAO] DEFAULT (getdate()) FOR [DATA_INCLUSAO];

