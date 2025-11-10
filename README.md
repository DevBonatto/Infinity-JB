♾️ Infinity Team - Jailbreak (CS2)

Este é o fork oficial do projeto Jailbreak da EdgeGamers mantido pela
Infinity Team.

O objetivo principal deste fork é fornecer uma tradução completa para
Português (PT-BR) e adicionar modificações customizadas para os nossos
servidores.

🚀 Status

-   Tradução PT-BR: Em progresso.
-   Sincronizado com (Upstream): edgegamers/Jailbreak

------------------------------------------------------------------------

🛠️ Como Compilar (Build)

Para fazer alterações no código ou compilar uma nova versão, siga estes
passos.

1. Pré-requisitos

-   Git
-   .NET 8.0 SDK

2. Clonar o Repositório

Para clonar o projeto, é essencial usar o comando --recurse-submodules
para baixar as dependências (como Jailbreak.Generic).

    git clone --recurse-submodules https://github.com/DevBonatto/Infinity-JB.git
    cd Infinity-JB

3. Compilar o Projeto

Você pode compilar usando a linha de comando (recomendado) ou o Visual
Studio.

Método A: Linha de Comando (Recomendado)

Este é o método mais rápido para gerar os arquivos de produção.

    # Navegue até a raiz do projeto e execute:
    dotnet publish src/Jailbreak/Jailbreak.csproj -c Release

Os arquivos compilados estarão na pasta build/Jailbreak.

Método B: Visual Studio 2022

Abra o arquivo Jailbreak.sln com o Visual Studio 2022.

Mude a configuração no menu superior de “Debug” para “Release”.

No “Gerenciador de Soluções” (menu à direita), clique com o botão
direito no projeto Jailbreak e selecione Publicar.

4. Implantar no Servidor

Após a compilação, pegue a pasta Jailbreak gerada dentro de build/ e
coloque-a no seu servidor:

    .../cs2-server/game/csgo/addons/counterstrikesharp/plugins/

------------------------------------------------------------------------

🔄 Sincronizando com o Projeto Original (Upstream)

Para manter este fork atualizado com as últimas correções e recursos do
edgegamers/Jailbreak, siga este processo:

1. Adicionar o “Upstream” (Apenas 1 vez)

Se você acabou de clonar, precisa adicionar o repositório “pai” como um
controle remoto:

    git remote add upstream https://github.com/edgegamers/Jailbreak.git

2. Puxar Atualizações (Fluxo de Trabalho)

Quando quiser buscar novas atualizações do projeto original:

    # 1. Busque as atualizações do 'pai'
    git fetch upstream

    # 2. Vá para o seu branch principal (ex: 'main')
    git checkout main

    # 3. Mescle as atualizações do 'pai' no seu branch
    # (O Git vai tentar juntar as mudanças. Resolva os conflitos se aparecerem)
    git merge upstream/main

    # 4. Envie as atualizações mescladas para o NOSSO repositório
    git push origin main

Após mesclar, lembre-se de compilar e implantar os arquivos .dll
novamente.

------------------------------------------------------------------------

📜 Licença

Este projeto é licenciado sob a GPL-3.0 License, assim como o
repositório original.
