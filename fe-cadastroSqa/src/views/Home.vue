<script setup>
  import { onMounted, computed, reactive, watchEffect } from "vue";

  import Swal from "sweetalert2";

  import useStore from "../store";
  import useModal from '../hooks/useModal';

  import fns from "../utils/formatadoresData";

  import { GET_USERS, UPDATE_DATA_USER, DELETE_USER } from "../store/type-actions";
  
  const store = useStore();
  const modal = useModal();
  const state = reactive({
    pagina: 1,
    totalPaginas: 1,
    users: [],
  });

  async function buscaCadastrosPaginados(){
    try{
      await store.dispatch(GET_USERS, state.pagina);
    }
    catch(error){
      console.log(error);
    }
  }

  onMounted(async () => {
    await buscaCadastrosPaginados();    
  })

  watchEffect(() => buscaCadastrosPaginados(), state.pagina);

  state.totalPaginas = computed(() => store.state.user.totalPaginas);

  function handlePaginacaoSoma() {
      state.pagina++;
  }

  function handlePaginacaoSubtracao() {
    state.pagina--;
  }

  state.users = computed(() => store.state.user.users);

  function handleAddUser(){
    modal.open({
      component: "ModalAddUser"
    })
  }

  async function handleUpdateUser(user){
    await store.dispatch(UPDATE_DATA_USER, user);
      modal.open({
        component: "ModalUpdateUser"
      })
  }

  async function confirmDeleteUser(id) {
     await store.dispatch(DELETE_USER, id);
      await Swal.fire({
        position: "center",
        icon: "success",
        iconColor: "white",
        color: "white",
        background: "#5061FC",
        title: `Sucesso!`,
        text: `Cadastro de código ${id} excluído com sucesso!`,
        showConfirmButton: false,
        timer: 2500,
      });
      await store.dispatch(GET_USERS, state.pagina);
      modal.close();
  }
  
   async function handleRemoveUser(id) {
       await Swal.fire({
        title: "Excluir Cadastro?",
        text: "Essa ação é irreversível!",
        icon: "warning",
        iconColor: "white",
        color: "white",
        background: "#5061FC",
        showCancelButton: true,
        confirmButtonColor: "#d73035",
        cancelButtonColor: "#6674f4",
        cancelButtonText: "Cancelar",
        confirmButtonText: "Sim, excluir cadastro.",
      }).then((result) => {
        if (result.isConfirmed) {
          confirmDeleteUser(id);
        }
      });
    }

  function formatarTexto(texto){
      if (!texto) return;
      return texto.toLowerCase().replace(/(^|\s|~)\S/g, l => l.toUpperCase());
    };
</script>

<template>
  <div v-if="state.users && state.users.length" class="container">
      <div class="add-button">
        <button @click="handleAddUser">
          Novo cadastro
        </button>
      </div>
      <table>
        <thead>
          <tr>
            <th>Nome</th>
            <th>CPF</th>
            <th>Idade</th>
            <th>Endereço</th>
            <th>Sexo</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="user in state.users" :key="user.id">
            <td>{{ formatarTexto(user.name) }}</td>
            <td>{{ user.cpf }}</td>
            <td> {{ fns.calcularIdade(user.dataNascimento) }} anos</td>
            <td>{{ formatarTexto(user.endereco) }}</td>
            <td>{{ user.sexo }}</td>
            <td>
              <div class="actions">
                <img @click="handleUpdateUser(user)" class="icons" src="../assets/img/edit.svg" alt="Editar cadastro" title="Editar cadastro">
                <img @click="handleRemoveUser(user.id)" class="icons" src="../assets/img/trash.svg" alt="Excluir cadastro" title="Excluir cadastro">
              </div>
            </td>
          </tr>
        </tbody>
      </table>
  
      <div class="pagination">
          <button
            :disabled="state.pagina === 1"
            class="button-pagination"
            @click="handlePaginacaoSubtracao"
          >
              &lt;
          </button>
          <button class="button-pagination numero-pagina">
              {{ state.pagina }}
          </button>
          <button
            :disabled="state.pagina >= state.totalPaginas"
            class="button-pagination"
            @click="handlePaginacaoSoma"
          >
              &gt;
          </button>
      </div>

    </div>
  
    <div v-else class="empty-list-container">
      <img src="../assets/img/empty-box.svg" alt="Caixa vazia">
      <p class="empty-list-paragraph">
        Ainda não há cadastros, para começar clique em
        <button @click="handleAddUser">
          Novo cadastro
        </button>
      </p>
    </div>

</template>

<style scoped>
  div.container {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    width: 56.25rem;
  }

  button {
    background-color: var(--main);
    border: none;
    border-radius: 5px;
    padding: 10px 20px;

    color: var(--white);
    text-decoration: none;
    font-weight: bold;
    text-shadow: 2px 2px 0px rgba(0,0,0,0.3);

    cursor: pointer;
  }

  div.add-button{
    margin: 1rem 0;
    position: relative;
    right: 23rem;
  }
  
  table {
    width: 100%;
    border-radius: 0.5rem;
  }
  
  table, th, td {
    border: 0.125rem solid var(--main-dark);
    border-collapse: collapse;
    padding: 0.625rem;
    text-align: left;
  }

  th {
    background-color: var(--main-light);
    color: white;
    text-shadow: 2px 2px 0px rgba(0,0,0,0.3);
    font-weight: bold;
  }
  
  tr {
    height: 3rem;
  }
  
  tr:nth-child(odd){
    background-color: var(--lighter);
  }
  
  td {
    text-shadow: 1px 1px 0px rgba(0,0,0,0.1);
  }
  .actions {
    display: flex;
    align-items: center;
    justify-content: space-evenly;
  }

  .icons:hover {
    cursor: pointer;
    opacity: 75%;
  }

  div.pagination {
  display: flex;
  position: absolute;
  width: 30%;
  align-items: center;
  justify-content: space-between;
  padding: 0 0.75rem;
  bottom: 0;
}

.button-pagination {
  color: var(--main-dark);
  font-size: 2rem;
  background-color: var(--white);
  border: none;
  cursor: pointer;
  font-weight: bold;
}

.button-pagination[disabled] {
  cursor: default;
  color: var(--gray-light);
}

.numero-pagina {
  font-size: 1.3rem;
  font-weight: bold;
}

div.empty-list-container{
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-top: 15rem;
}

p.empty-list-paragraph {
  color: var(--main-dark);
  font-weight: bold;
  font-size: 1.125rem;
  
  margin-top: 2rem;
}

small.empty-list-small {
  background-color: var(--main-dark);
  color: var(--white);
}
</style>