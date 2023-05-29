<script setup>
import { reactive, computed } from "vue";

import useStore  from "../../store";
import useModal from "../../hooks/useModal";
import Swal from "sweetalert2";

import fns from "../../utils/formatadoresData";

import { CREATE_USER, GET_USERS } from "../../store/type-actions";

const store = useStore();
const modal = useModal();

  const state = reactive({
    name: "",
    cpf: "",
    dataNascimento: "",
    endereco: "",
    sexo: "",
    isSaveButtonDisabled: false,
    user: {}
  })

  const getCurrentDate = computed(() => {
    const hoje = new Date();
    const ano = hoje.getFullYear();
    let mes = hoje.getMonth() + 1;
    if (mes < 10) {
      mes = '0' + mes;
    }
    let dia = hoje.getDate();
    if (dia < 10) {
      dia = '0' + dia;
    }
    return `${ano}-${mes}-${dia}`;
  });

   async function handleSubmitForm() {
    try {
      state.isSaveButtonDisabled = true;
      if(!state.cpf || !state.endereco || !state.sexo || !state.dataNascimento){
          await Swal.fire({
          position: "center",
          icon: "error",
          iconColor: "white",
          background: "#5061FC",
          color: "white",
          title: `Tente novamente.`,
          text: `Campos obrigatórios a serem preenchidos.`,
          showConfirmButton: false,
          timer: 2500,
        });
        state.isSaveButtonDisabled = false;
          return;
      }
      const maioridade = fns.verificarMaioridade(state.dataNascimento);
      if(!maioridade){
        await Swal.fire({
          position: "center",
          icon: "error",
          iconColor: "white",
          background: "#5061FC",
          color: "white",
          title: `Tente novamente.`,
          text: `A data de nascimento informada representa um menor de idade.`,
          showConfirmButton: false,
          timer: 2500,
        });
        state.isSaveButtonDisabled = false;
          return;
      }
      state.user = {
        name: state.name,
        cpf: state.cpf,
        dataNascimento: fns.formatDateToString(state.dataNascimento),
        endereco: state.endereco,
        sexo: state.sexo
      };
      const resultado = await store.dispatch(CREATE_USER, state.user);
      if(!resultado){
        await Swal.fire({
          position: "center",
          icon: "error",
          iconColor: "white",
          color: "white",
          background: "#5061FC",
          title: `Falha ao cadastrar usuário.`,
          text: `Verifique os dados informados e tente novamente!`,
          showConfirmButton: false,
          timer: 2500,
        });
        return;
      }
      await Swal.fire({
          position: "center",
          icon: "success",
          iconColor: "white",
          color: "white",
          background: "#5061FC",
          title: `Sucesso!`,
          text: `Cadastro do usuário ${state.name} efetuado com sucesso!`,
          showConfirmButton: false,
          timer: 2500,
        });
        await store.dispatch(GET_USERS, 1);
        modal.close();
        state.isSaveButtonDisabled = false;
    } catch (error) {
      console.log(error);
    } finally {
      state.isSaveButtonDisabled = false;
    }
  }

</script>

<template>
  <button @click="modal.close" class="close-button">&times;</button>
  <div class="title-form">
    <h1>
      Novo cadastro
    </h1>
  </div>

  <div class="form-div">
    <form @submit.prevent="handleSubmitForm">
      <div class="form-group">
        <label for="nome">Nome:</label>
        <input type="text" id="nome" v-model="state.name" />
      </div>
      <div class="form-group">
        <label for="cpf">CPF:</label>
        <input 
          type="text" 
          id="cpf" 
          v-model="state.cpf"
          placeholder="___.___.___-__"
        />
      </div>
      <div class="form-group">
        <label for="data-nascimento">Data de Nascimento:</label>
        <input type="date" id="data-nascimento" :max="getCurrentDate" v-model="state.dataNascimento" />
      </div>
      <div class="form-group">
        <label for="endereco">Endereço:</label>
        <input type="text" id="endereco" v-model="state.endereco" />
      </div>
      <div class="form-group">
        <label>Sexo:</label>
        <div class="radio-group">
          <label for="masculino">
            <input type="radio" id="masculino" value="Masculino" v-model="state.sexo" />
            Masculino
          </label>
          <label for="feminino">
            <input type="radio" id="feminino" value="Feminino" v-model="state.sexo" />
            Feminino
          </label>
        </div>
      </div>

      <div class="button-submit">
        <button 
          type="submit"
          :disabled="state.isSaveButtonDisabled"
          id="submit-button"
        >
          Gravar
        </button>
        <button type="button" @click="modal.close">Cancelar</button>
      </div>
    </form>
  </div>

</template>

<style scoped>
.form-div {
  max-width: 400px;
  margin: 0 auto;
  padding: 20px;
  background-color: var(--lighter);
  border-radius: 5px;
}

.form-group {
  margin-bottom: 20px;
  color: var(--main);
}

div.title-form, h1 {
  font-size: 1.5rem;
  display: flex;
  margin-top: 0.5rem;
  color: var(--main);
  text-shadow: 2px 2px 0px rgba(0,0,0,0.1);
}

label {
  font-weight: bold;
  display: block;
  text-shadow: 2px 2px 0px rgba(0,0,0,0.1);
}

input[type="text"],
input[type="date"] {
  width: 100%;
  padding: 0.625rem 1rem 0.625rem 0.625rem;
  border: 1px solid var(--main);
  border-radius: 5px;
  outline-color: var(--main-dark);
}

input[type="radio"] {
  outline: none;
}

input[type="text"]::placeholder{
  color: var(--main-dark);
}

.radio-group {
  display: flex;
  gap: 10px;
}

.button-submit {
  display: flex;
  justify-content: center;
}

#submit-button[disabled]{
  background: var(--gray-dark);
  cursor: default;
}

button {
  padding: 10px 20px;
  color: var(--white);
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-weight: bold;
  text-decoration: none;
  text-shadow: 2px 2px 0px rgba(0,0,0,0.3);
  outline: none;
}

button.close-button {
  border: 2px solid transparent;
  background-color: transparent;
  outline: none;
  color: var(--main-dark);
  font-size: 2.25rem;
  position: absolute;
  right: 1rem;
  top: 0.5rem;
  cursor: pointer;
}

button[type="submit"] {
  background-color: var(--main);
}

button[type="button"] {
  background-color: var(--main-red);
}
</style>
