CREATE TABLE [dbo].[OPCOES_RESPOSTA_QUESTIONARIOS] (
    [ID_OPCAO_RESPOSTA_QUESTIONARIO] NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
    [ID_OPCAO_RESPOSTA]              NUMERIC (18)  NOT NULL,
    [ID_QUESTIONARIO]                NUMERIC (18)  NOT NULL,
    [USUARIO_INCLUSAO]               NVARCHAR (50) NULL,
    [DATA_INCLUSAO]                  DATE          NULL,
    [USUARIO_ALTERACAO]              NVARCHAR (50) NULL,
    [DATA_ALTERACAO]                 DATE          NULL,
    [EXCLUIDO]                       INT           NULL
);

