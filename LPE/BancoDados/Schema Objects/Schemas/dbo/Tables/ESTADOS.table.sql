CREATE TABLE [dbo].[ESTADOS] (
    [ID_ESTADO]         NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
    [SIGLA]             CHAR (2)      NOT NULL,
    [NOME]              VARCHAR (60)  NOT NULL,
    [USUARIO_INCLUSAO]  NVARCHAR (50) NULL,
    [DATA_INCLUSAO]     DATE          NULL,
    [USUARIO_ALTERACAO] NVARCHAR (50) NULL,
    [DATA_ALTERACAO]    DATE          NULL,
    [EXCLUIDO]          INT           NULL
);



