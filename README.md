
---

# EmployeeAPI - Configuração e Migrações

## Requisitos
- MS SQL Server
- .NET Core SDK

## Passos para Configuração

### 1. Baixe o RGDB para o MS SQL Server

### 2. Configure a String de Conexão

Altere o caminho da conexão para a sua conexão atual no arquivo `appsettings.json`. A minha conexão é:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data source=DESKTOP-USMHLML\\SQLEXPRESS;database=EmployeeAPI;Trusted_connection=true;Encrypt=false;TrustServerCertificate=true"
  }
}
```

Provavelmente a sua conexão será diferente. Aqui está um exemplo de configuração padrão:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MyDatabase;Trusted_Connection=True;"
  }
}
```

### 3. Executar Migrações no Banco de Dados

Execute os seguintes comandos no terminal para gerenciar as migrações e atualizar o banco de dados:

#### Adicionar uma Migração Inicial
```bash
dotnet ef migrations add InitialCreate
```

#### Atualizar o Banco de Dados
```bash
dotnet ef database update
```

#### Adicionar uma Nova Migração Após Modificar o Modelo
```bash
dotnet ef migrations add AddNewColumn
```

#### Atualizar o Banco de Dados Após Adicionar a Nova Migração
```bash
dotnet ef database update
```

## Notas Adicionais
- Certifique-se de que o MS SQL Server está em execução e acessível.
- Verifique se a string de conexão está corretamente configurada para refletir seu ambiente de desenvolvimento.

---

Seguindo esses passos, você será capaz de configurar e gerenciar as migrações da sua API com o Entity Framework Core e MS SQL Server.
