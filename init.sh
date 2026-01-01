#!/bin/bash

# helper functions

COLOR_GREEN="\033[0;32m"
COLOR_YELLOW="\033[0;33m"
COLOR_RED="\033[0;31m"
COLOR_RESET="\033[0m"

info() {
  printf "[%bINFO%b] %s\n" "$COLOR_GREEN" "$COLOR_RESET" "$1"
}

warn() {
  printf "[%bWARN%b] %s\n" "$COLOR_YELLOW" "$COLOR_RESET" "$1" >&2
}

error() {
  printf "[%bERRO%b] %s\n" "$COLOR_RED" "$COLOR_RESET" "$1" >&2
}

askWithDefault() {
  local prompt="$1"
  local default="$2"
  local input

  read -r -p "$prompt [default: $default]: " input
  if [[ -n "$input" ]]; then
    printf '%s\n' "$input"
    return 0
  else
    printf '%s\n' "$default"
    return 0
  fi
}

askNonNull() {
  local prompt="$1"
  local input

  while true; do
    read -r -p "$prompt: " input
    if [[ -n "$input" ]]; then
      printf '%s\n' "$input"
      return 0
    else
      printf 'Warning: value cannot be empty. Please try again.\n' >&2
    fi
  done
}

askInList() {
  local prompt="$1"
  local default_index="$2"
  shift 2
  local options=("$@")
  local input

  # Display the options with numbers
  printf '%s\n' "$prompt" >&2
  for i in "${!options[@]}"; do
    printf '  %d) %s\n' "$((i + 1))" "${options[$i]}" >&2
  done

  # Determine default string for the prompt display
  local default_val="${options[$((default_index - 1))]}"

  while true; do
    read -r -p "Select an option [1-${#options[@]}, default: $default_index ($default_val)]: " input
    if [[ -z "$input" ]]; then
      input="$default_index"
    fi

    # Validate that input is a number and within range
    if [[ "$input" =~ ^[0-9]+$ ]] && ((input >= 1 && input <= ${#options[@]})); then
      printf '%s\n' "${options[$((input - 1))]}"
      return 0
    else
      warn "Please enter a valid number between 1 and ${#options[@]}"
    fi
  done
}

generateLicense() {
  local name="$GIT_USER"
  local year=$(date +%Y)
  local type="$1"
  local out_dir="./$NAMESPACE"

  info "Downloading $type license template..."
  local url="https://raw.githubusercontent.com/github/choosealicense.com/gh-pages/_licenses/${type}.txt"
  curl -s "$url" |
    sed '1,/^---$/d' |
    sed -e "s/\[year\]/$year/g" \
      -e "s/\[fullname\]/$name/g" \
      -e "s/\[yyyy\]/$year/g" \
      -e "s/\[name of copyright owner\]/$name/g" \
      >"${out_dir}/LICENSE"

  info "Success: LICENSE created in $out_dir"
}

kebabToPascal() {
  local input="$1"
  local output=""
  local part

  IFS='-' read -ra parts <<<"$input"

  for part in "${parts[@]}"; do
    [[ -z "$part" ]] && continue
    output+="${part^}"
  done

  printf '%s\n' "$output"
}

toWords() {
  local input="$1"
  local output
  if [ -z "$input" ]; then
    echo ""
    return 1
  fi
  output=$(echo "$input" | sed -E \
    -e 's/([A-Z]+)([A-Z][a-z])/\1 \2/g' \
    -e 's/([a-z0-9])([A-Z])/\1 \2/g')
  echo "$output"
}

replaceInFiles() {
  local search="$1"
  local replace="$2"
  local self
  self="./$(basename "${BASH_SOURCE[0]}")"
  find . \
    -path "./Sandbox.$NAMESPACE/Library" -prune -o \
    -path "./Sandbox.$NAMESPACE/Logs" -prune -o \
    -path "./Sandbox.$NAMESPACE/Temp" -prune -o \
    -path "./Sandbox.$NAMESPACE/obj" -prune -o \
    -path "./init.*" -prune -o \
    -type d -name .git -prune -o \
    -type f ! -path "$self" -print0 |
    xargs -0 grep -Il "$search" |
    xargs sed -i "s/${search//\//\\/}/${replace//\//\\/}/g"
}

renameDirs() {
  local search="$1"
  local replace="$2"
  find . -depth \
    -path "./Sandbox.$NAMESPACE/Library" -prune -o \
    -path "./Sandbox.$NAMESPACE/Logs" -prune -o \
    -path "./Sandbox.$NAMESPACE/Temp" -prune -o \
    -path "./Sandbox.$NAMESPACE/obj" -prune -o \
    -type d -name "*$search*" -print0 |
    while IFS= read -r -d '' dir; do
      local newdir="${dir//$search/$replace}"
      mv "$dir" "$newdir"
    done
}

renameFiles() {
  local search="$1"
  local replace="$2"
  find . \
    -path "./Sandbox.$NAMESPACE/Library" -prune -o \
    -path "./Sandbox.$NAMESPACE/Logs" -prune -o \
    -path "./Sandbox.$NAMESPACE/Temp" -prune -o \
    -path "./Sandbox.$NAMESPACE/obj" -prune -o \
    -type f -name "*$search*" -print0 |
    while IFS= read -r -d '' file; do
      local newfile="${file//$search/$replace}"
      mv "$file" "$newfile"
    done
}

toLower() {
  local input="$1"
  local lower
  lower=$(printf '%s' "$input" | tr '[:upper:]' '[:lower:]')
  if [[ "$input" != "$lower" ]]; then
    warn "Uppercase letters detected. Unity package IDs must be lowercase. Converting to lowercase."
  fi
  printf '%s\n' "$lower"
}

toLowerPure() {
  printf '%s\n' "$(printf '%s' "$1" | tr '[:upper:]' '[:lower:]')"
}

getGithubUser() {
  local user
  user=$(git remote get-url origin 2>/dev/null |
    sed -E 's#.*github.com[:/](.+)/.*#\1#')
  if [[ -n "$user" ]]; then
    printf '%s\n' "$user"
    return 0
  fi
  if command -v gh >/dev/null 2>&1; then
    user=$(gh api user --jq '.login' 2>/dev/null)
    [[ -n "$user" ]] && printf '%s\n' "$user" && return 0
  fi
  warn "Unable to determine GitHub username. Falling back to git user.name."
  git config user.name
}

unityStartup() {
  "$UNITY_PATH" \
    -batchmode \
    -nographics \
    -projectPath "$PROJECT_PATH" \
    -logFile "unity_init.log" \
    -quit >/dev/null 2>&1

  if [ $? -eq 0 ]; then
    info "Unity initialization complete."
  else
    error "Unity initialization failed. Check unity_init.log for details."
    tail -n 20 unity_init.log
    exit 1
  fi
}

uploadSecrets() {
  # --- 1. Check for GitHub CLI ---
  if ! command -v gh &>/dev/null; then
    warn "GitHub CLI (gh) not found. Please install it to automate secret setup."
    exit 1
  fi
  # --- 2. Check Auth Status ---
  if ! gh auth status &>/dev/null; then
    warn "You are not logged into GitHub CLI. Running 'gh auth login'..."
    gh auth login
  fi

  info "Starting Secrets Setup..."
  # --- 3. Unity License Setup ---
  # Check if they have a license file locally to upload
  DEFAULT_LICENSE="$HOME/.local/share/unity3d/Unity/Unity_lic.ulf"
  licenseFile=$(askWithDefault "Insert the path to the Unity license" "$DEFAULT_LICENSE")
  licenseFile="${licenseFile/#\~/$HOME}"
  unityEmail=$(askWithDefault "Insert your unity email" $GIT_MAIL)
  unityPassword=$(askNonNull "Insert your unity password")
  sonarToken=$(askNonNull "Insert your sonar qube token")
  sonarUrl=$(askWithDefault "Insert your sonar qube url" "https://sonarcloud.io")
  if [[ -f "$licenseFile" ]]; then
    if gh secret set UNITY_LICENSE <"$licenseFile"; then
      info "Unity License uploaded successfully."
    else
      error "Failed to upload license via gh cli."
    fi
  else
    warn "License file not found at: $licenseFile. Skipping UNITY_LICENSE setup."
  fi
  if gh secret set UNITY_EMAIL --body "$unityEmail"; then
    info "Unity email uploaded successfully."
  else
    error "Failed to upload email via gh cli."
  fi
  if gh secret set UNITY_PASSWORD --body "$unityPassword"; then
    info "Unity password uploaded successfully."
  else
    error "Failed to upload password via gh cli."
  fi
  if gh secret set SONAR_TOKEN --body "$sonarToken"; then
    info "Sonar token uploaded successfully."
  else
    error "Failed to upload sonar token via gh cli."
  fi
  if gh secret set SONAR_HOST_URL --body "$sonarUrl"; then
    info "Sonar url uploaded successfully."
  else
    error "Failed to upload sonar url via gh cli."
  fi
  echo "Secrets configured successfully!"
}

#---------------------------------------------------------------------------------------------------

# Read customer values
GIT_USER=$(getGithubUser)
GIT_MAIL=$(git config user.email)
DOMAIN=$(toLower "$(askWithDefault "Enter the top level domain" "com")")
COMPANY=$(toLower "$(askWithDefault "Enter your company name" "$(toLowerPure "$GIT_USER")")")
PACKAGE=$(toLower "$(askNonNull "Enter your package name (e.g. 'awesome-tool')")")
NAMESPACE=$(askWithDefault "Enter the default namespace" $(kebabToPascal "$PACKAGE"))
DESCRIPTION=$(askWithDefault "Enter a description" "")
NAME=$(toWords "$NAMESPACE")
LICENSE=$(askInList "Select a license type:" 1 "mit" "apache-2.0" "gpl-3.0" "isc")

info "The resulting package unique ID is $DOMAIN.$COMPANY.$PACKAGE"
info "The namespace is $NAMESPACE"
info "The package display name is $NAME"
info "The license is $LICENSE"

uploadSecrets

# Replace all directories with matching pattern
info "Renaming dirs with __DOMAIN__=$DOMAIN"
renameDirs "__DOMAIN__" "$DOMAIN"
info "Renaming dirs with __COMPANY__=$COMPANY"
renameDirs "__COMPANY__" "$COMPANY"
info "Renaming dirs with __PACKAGE__=$PACKAGE"
renameDirs "__PACKAGE__" "$PACKAGE"
info "Renaming dirs with __NAMESPACE__=$NAMESPACE"
renameDirs "__NAMESPACE__" "$NAMESPACE"
info "Renaming dirs with __NAME__=$NAME"
renameDirs "__NAME__" "$NAME"

# Rename all files with matching pattern
info "Renaming files with __DOMAIN__=$DOMAIN"
renameFiles "__DOMAIN__" "$DOMAIN"
info "Renaming files with __COMPANY__=$COMPANY"
renameFiles "__COMPANY__" "$COMPANY"
info "Renaming files with __PACKAGE__=$PACKAGE"
renameFiles "__PACKAGE__" "$PACKAGE"
info "Renaming files with __NAMESPACE__=$NAMESPACE"
renameFiles "__NAMESPACE__" "$NAMESPACE"
info "Renaming files with __NAME__=$NAME"
renameFiles "__NAME__" "$NAME"

# Replace words in all files
info "Replacing words with __DOMAIN__=$DOMAIN"
replaceInFiles "__DOMAIN__" "$DOMAIN"
info "Replacing words with __COMPANY__=$COMPANY"
replaceInFiles "__COMPANY__" "$COMPANY"
info "Replacing words with __PACKAGE__=$PACKAGE"
replaceInFiles "__PACKAGE__" "$PACKAGE"
info "Replacing words with __NAMESPACE__=$NAMESPACE"
replaceInFiles "__NAMESPACE__" "$NAMESPACE"
info "Replacing words with __NAME__=$NAME"
replaceInFiles "__NAME__" "$NAME"
info "Replacing words with __DESCRIPTION__=$DESCRIPTION"
replaceInFiles "__DESCRIPTION__" "$DESCRIPTION"
info "Replacing words with __GIT_USER__=$GIT_USER"
replaceInFiles "__GIT_USER__" "$GIT_USER"
info "Replacing words with __GIT_MAIL__=$GIT_MAIL"
replaceInFiles "__GIT_MAIL__" "$GIT_MAIL"

# Install deps
info "Installing dotnet and npm dependencies"
npm i
dotnet tool restore

# Create LICENSE file
info "Creating LICENSE file"
generateLicense $LICENSE

PROJECT_VERSION=$(
  sed -n 's/^m_EditorVersion: //p' \
    "./Sandbox.$NAMESPACE/ProjectSettings/ProjectVersion.txt"
)
UNITY_EDITOR_DIR="$HOME/Unity/Hub/Editor"
if [[ -n "$PROJECT_VERSION" && -d "$UNITY_EDITOR_DIR/$PROJECT_VERSION" ]]; then
  INSTALLED_UNITY_VERSION="$PROJECT_VERSION"
else
  INSTALLED_UNITY_VERSION=$(
    find "$UNITY_EDITOR_DIR" \
      -mindepth 1 \
      -maxdepth 1 \
      -type d \
      -printf "%f\n" |
      sort -V |
      tail -n1
  )
fi

project_major=${PROJECT_VERSION%%.*}
installed_major=${INSTALLED_UNITY_VERSION%%.*}
if [[ "$project_major" -gt "$installed_major" ]]; then
  warn "Project requires Unity $PROJECT_VERSION, but installed Unity is $INSTALLED_UNITY_VERSION"
  warn "Skipping unity opening, install unity editor $PROJECT_VERSION before starting to develop"
else
  UNITY_PATH="$HOME/Unity/Hub/Editor/$INSTALLED_UNITY_VERSION/Editor/Unity"
  PROJECT_PATH=$(realpath "./Sandbox.$NAMESPACE")

  info "Initializing Unity project $NAMESPACE - this operation may take a few minutes..."
  unityStartup
fi

# Install hooks
info "Installing git hooks"
npx lefthook install

# Remove template marker file. The execution of this script means that the project is being used as a product, not developed
info "Removing .template file"
rm .template

# Auto remotion
info "Removing init files since their not needed anymore"
rm init.sh
rm init.ps1

info "Generating $NAMESPACE.csproj in order to let docfx work"
node Tools/generate-csproj-for-docfx.js

info "Re-launching unity startup just to generate meta file related to new csproj"
unityStartup

info "Opening Unity Editor GUI"
"$UNITY_PATH" -projectPath "$PROJECT_PATH" &

info "Committing changes"
git add .
git commit -m "chore(init): initialize project from template"

info "Setting this commit as version 0.0.0"
git tag 0.0.0

info "Init done, remember to configure precisely the $NAMESPACE/package.json file before starting your development."
info "Set LICENSE before publishing"
info "Remember to push tags too (git push --tags)"
