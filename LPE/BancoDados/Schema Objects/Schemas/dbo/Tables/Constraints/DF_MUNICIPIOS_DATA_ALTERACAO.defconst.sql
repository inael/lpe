﻿ALTER TABLE [dbo].[MUNICIPIOS]
    ADD CONSTRAINT [DF_MUNICIPIOS_DATA_ALTERACAO] DEFAULT (getdate()) FOR [DATA_ALTERACAO];

