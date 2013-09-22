CREATE TABLE [dbo].[CBO_GRUPOS] (
    [ID_GRUPO]          NUMERIC (18)   NOT NULL,
    [DESCRICAO]         NVARCHAR (300) NOT NULL,
    [DATA_INCLUSAO]     DATE           NULL,
    [USUARIO_ALTERACAO] NVARCHAR (50)  NULL,
    [DATA_ALTERACAO]    DATE           NULL,
    [EXCLUIDO]          INT            NULL
);

