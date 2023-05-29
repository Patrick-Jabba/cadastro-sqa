const functions = {
  calcularIdade: function (dataNascimento) {
    const dataAtual = new Date();
    const anoAtual = dataAtual.getUTCFullYear();
    const mesAtual = dataAtual.getUTCMonth() + 1;
    const diaAtual = dataAtual.getUTCDate();
  
    const dataNasc = new Date(dataNascimento);
    const anoNascimento = dataNasc.getUTCFullYear();
    const mesNascimento = dataNasc.getUTCMonth() + 1;
    const diaNascimento = dataNasc.getUTCDate();
  
    let idade = anoAtual - anoNascimento;
  
    if (mesAtual < mesNascimento || (mesAtual === mesNascimento && diaAtual < diaNascimento)) {
      idade--;
    }
  
    return idade;
  },

  formatDateToString: function(date) {
    const dateFormated = new Date(date);

    return dateFormated.toISOString();
  },

  formatDateToView: function (date) {
    const dateFormated = new Date(date);

    const timeDiff = dateFormated.getTimezoneOffset() * 60000;

    const adjustedDate = new Date(dateFormated.valueOf() + timeDiff);

    return adjustedDate.toLocaleDateString().substring(0, 10);
  },

  formatDateToViewWithDateInput: function (date) {
    const data = new Date(date);
    const timeDiff = data.getTimezoneOffset() * 60000;
    const dataAjustada = new Date(data.valueOf() + timeDiff);
    const year = dataAjustada.getFullYear();
    const month = (dataAjustada.getMonth() + 1).toString().padStart(2, '0');
    const day = (dataAjustada.getDate().toString().padStart(2, '0'));
    const formattedDateString = `${year}-${month}-${day}`;
    return formattedDateString;
  },

  verificarMaioridade: function(data) {
    const partesData = data.split("-");
    const ano = parseInt(partesData[0]);
    const mes = parseInt(partesData[1]) - 1;
    const dia = parseInt(partesData[2]);
  
    const dataNascimento = new Date(ano, mes, dia);
    const dataAtual = new Date();
  
    // Adicionar 18 anos à data de nascimento
    dataNascimento.setFullYear(dataNascimento.getFullYear() + 18);
  
    // Verificar se a data atual é maior ou igual à data de maioridade
    return dataAtual >= dataNascimento;
  }
}

export default functions;