﻿ALTER TABLE [dbo].[ESTADOS]
    ADD CONSTRAINT [DF_ESTADOS_EXCLUIDO] DEFAULT ((0)) FOR [EXCLUIDO];

