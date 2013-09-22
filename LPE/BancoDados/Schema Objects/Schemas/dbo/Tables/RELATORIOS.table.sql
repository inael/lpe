CREATE TABLE [dbo].[RELATORIOS] (
    [ID_RELATORIO]      NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
    [ID_GRUPO]          NUMERIC (18)  NULL,
    [ID_NIVEL]          NUMERIC (18)  NULL,
    [CARACTERISTICA]    TEXT          NULL,
    [MOTIVA]            TEXT          NULL,
    [DESAGRADA]         TEXT          NULL,
    [POTENCIAL]         TEXT          NULL,
    [VALOR_MIN]         NUMERIC (18)  NULL,
    [VALOR_MAX]         NUMERIC (18)  NULL,
    [USUARIO_INCLUSAO]  NVARCHAR (50) NULL,
    [DATA_INCLUSAO]     DATE          NULL,
    [USUARIO_ALTERACAO] NVARCHAR (50) NULL,
    [DATA_ALTERACAO]    DATE          NULL,
    [EXCLUIDO]          INT           NULL
);



