﻿ALTER TABLE [dbo].[CBO_FAMILIAS]
    ADD CONSTRAINT [FK_CBO_FAMILIA_CBO_SUB_GRUPO] FOREIGN KEY ([ID_SUB_GRUPO]) REFERENCES [dbo].[CBO_SUB_GRUPOS] ([ID_SUB_GRUPO]) ON DELETE NO ACTION ON UPDATE NO ACTION;
