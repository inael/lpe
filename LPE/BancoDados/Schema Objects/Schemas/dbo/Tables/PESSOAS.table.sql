CREATE TABLE [dbo].[PESSOAS] (
    [ID_PESSOA]         NUMERIC (18)   IDENTITY (1, 1) NOT NULL,
    [ID_ENDERECO]       NUMERIC (18)   NULL,
    [ID_ESCOLARIDADE]   NUMERIC (18)   NULL,
    [ID_SINONIMO]       NUMERIC (18)   NULL,
    [ID_ESTADO_CIVIL]   NUMERIC (18)   NULL,
    [NOME_PESSOA]       NVARCHAR (50)  NULL,
    [SOBRENOME_PESSOA]  NVARCHAR (80)  NULL,
    [TRATAMENTO_PESSOA] NVARCHAR (10)  NULL,
    [EMAIL_PESSOA]      NVARCHAR (256) NULL,
    [CPF]               CHAR (14)      NULL,
    [IDADE]             INT            NULL,
    [DATA_NASCIMENTO]   DATE           NULL,
    [SEXO]              CHAR (1)       NULL,
    [USUARIO_INCLUSAO]  NVARCHAR (50)  NULL,
    [DATA_INCLUSAO]     DATE           NULL,
    [USUARIO_ALTERACAO] NVARCHAR (50)  NULL,
    [DATA_ALTERACAO]    DATE           NULL,
    [EXCLUIDO]          INT            NULL
);









