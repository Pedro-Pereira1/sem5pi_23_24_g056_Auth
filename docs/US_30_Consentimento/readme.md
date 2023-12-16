# US 30 - Give my consent or not to the collection and processing of my personal data;

## 1. Context

* This is the first time this task is being developed

## 2. Requirements

**Dependencies:**
- **US 20 -** Como potencial utente do sistema (ex., aluno, docente) pretendo registar-me como utente do sistema.

## 3. Analysis

**Regarding this requirement we understand that:**
To implement this US we need to have a checkbox and a link.
The link show to the user the processing of his personal data.
The checkbox is marked when he agrees with that.

### Questions to the client
> Q: Caso o utilizador não consinta à recolha de dados, qual seria a ação a desencadear por parte da aplicação?
> 
> A: não deve prosseguir o registo e deve ser apresentada uma mensagem ao utilizador

> Q: Na Us de registo de um utente na aplicação deve ser apresentado a este a política de privacidade antes ou depois de ele preencher a sua informação? E caso o mesmo não a aceite como devo proceder, aviso que o registo não é possível sem aceitar a política de privacidade e retorno à home page ou pergunto se se quer registar de novo?
> 
> A: No formulário de registo deve ser pedida toda a informação e apresentada uma checkbox para aceitação da política de privacidade. No texto dessa checkbox deve existir um link para a política de privacidade.
O preenchimento da checkbox é obrigatório e se não for preenchido deve ser apresentada uma mensagem


## 4. Implementation

#### RegisterComponent HTML

```
<div class="privacy">
                <input type="checkbox" id="checkbox" (change)="updatePrivacy($event)" />
                <label for="checkbox">I confirm that I have read the <a
                        href="https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstley">privacy
                        policy</a></label>
</div>
````

#### RegisterComponent

```
export class RegisterComponent {

  isChecked = false;

  constructor(private authService: AuthServiceService,
    private router: Router) { }

  registerForm = new FormGroup({
    name: new FormControl(""),
    email: new FormControl(""),
    phoneNumber: new FormControl(""),
    taxPayerNumber: new FormControl(""),
    password: new FormControl("")
  })

  onSubmit() {

    if(this.isChecked) {
      const user: RegisterUserDto = {
        name: this.registerForm.value.name!,
        email: this.registerForm.value.email!,
        phoneNumber: Number(this.registerForm.value.phoneNumber!),
        taxPayerNumber: Number(this.registerForm.value.taxPayerNumber!),
        password: this.registerForm.value.password!
      }
      this.authService.register(user).subscribe((user: UserDto) => {
        window.alert("User " + user.name + " created successfully");
      })
    } else {
      window.alert("You must accept the terms and conditions");
      return;
    }

  }


  updatePrivacy(event: Event) {
    const checkbox = event.target as HTMLInputElement;
    if (checkbox.checked) {
      this.isChecked = true;
    } else {
      this.isChecked = false;
    }
  }

}
````

## 5. Test

## 6. Observations

