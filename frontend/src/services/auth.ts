const API_URL = 'http://localhost:5000'

export type SignupRequest = {
  name: string
  email: string
  password: string
}


export async function signup({
  name,
  email,
  password,
}: SignupRequest) {
  const response = await fetch(`${API_URL}/auth/signup`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    credentials: 'include',
    body: JSON.stringify({
      name,
      email,
      password,
    }),
  })

  if (response.status === 409) {
    throw new Error('An account with this email already exists.')
  }

  if (!response.ok) {
    throw new Error('Could not create account.')
  }
}

export async function login(email: string, password: string) {
  const response = await fetch(`${API_URL}/auth/login`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    credentials: 'include',
    body: JSON.stringify({
      email,
      password,
    }),
  })

  if (!response.ok) {
    throw new Error('Invalid email or password.')
  }
}

export async function getCurrentUser() {
  const response = await fetch(`${API_URL}/auth/me`, {
    credentials: 'include',
  })

  if (!response.ok) {
    return null
  }

  return response.json()
}

export async function logout() {
  await fetch(`${API_URL}/auth/logout`, {
    method: 'POST',
    credentials: 'include',
  })
}