# cadastro-sqa
## Repositório para o projeto avaliação do processo seletivo time SQA na Alterdata Software

## O projeto consiste em criar um cadastro de pessoas, respeitando o mockup fornecido.

## 🐋 Rodando através do docker:
### Após clonar o projeto na raiz do repositório dê o comando :
#### • docker compose up --build, ou docker-compose up --build

## 💻 Localmente: 
### No diretório back-cadastroSqa/cadastroSqa dê o comando:
#### • dotnet restore 

#### • No mesmo diretório no arquivo Program.cs descomente a linha 27 e comente a linha 29, e gere através do dotnet secrets a sua connectionString com o banco postgres lembrando de passar na chave o nome da sua conexão, no caso "CONEXAODB" e no valor a connectionstring em si.

#### • Depois o comando dotnet watch run

### Para o frontend local no diretório fe-cadastroSqa dê o comando:
#### • npm install e após npm run dev 

### Para os testes do backend :
#### • dotnet test

### Para os testes no frontend :
#### • npm run test
