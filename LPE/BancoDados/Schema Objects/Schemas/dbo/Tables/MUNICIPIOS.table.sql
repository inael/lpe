CREATE TABLE [dbo].[MUNICIPIOS] (
    [ID_MUNICIPIO]      NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
    [ID_ESTADO]         NUMERIC (18)  NOT NULL,
    [NOME]              VARCHAR (60)  NOT NULL,
    [IBGE]              CHAR (7)      NOT NULL,
    [IDUF]              INT           NOT NULL,
    [USUARIO_INCLUSAO]  NVARCHAR (50) NULL,
    [DATA_INCLUSAO]     DATE          NULL,
    [USUARIO_ALTERACAO] NVARCHAR (50) NULL,
    [DATA_ALTERACAO]    DATE          NULL,
    [EXCLUIDO]          INT           NULL
);



