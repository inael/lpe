﻿ALTER TABLE [dbo].[USUARIOS]
    ADD CONSTRAINT [DF_USUARIOS_DATA_ALTERACAO] DEFAULT (getdate()) FOR [DATA_ALTERACAO];

