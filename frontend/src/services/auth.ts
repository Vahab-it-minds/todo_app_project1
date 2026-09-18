const API_URL = 'http://localhost:5000'

export type SignupRequest = {
  name: string
  email: string
  password: string
}

export type User = {
  id: number
  name: string
  email: string
}

export type UpdateProfileRequest = {
  name: string
  email: string
}

export type ChangePasswordRequest = {
  currentPassword: string
  newPassword: string
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

export async function getCurrentUser(): Promise<User | null> {
  const response = await fetch(`${API_URL}/auth/me`, {
    credentials: 'include',
  })

  if (!response.ok) {
    return null
  }

  return response.json()
}

export async function updateProfile(
  profile: UpdateProfileRequest
): Promise<User> {
  const response = await fetch(`${API_URL}/users/me`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
    },
    credentials: 'include',
    body: JSON.stringify(profile),
  })

  if (response.status === 409) {
    throw new Error(
      'This email belongs to another account.'
    )
  }

  if (!response.ok) {
    throw new Error('Could not update profile.')
  }

  return response.json()
}

export async function changePassword(
  password: ChangePasswordRequest
): Promise<void> {
  const response = await fetch(
    `${API_URL}/users/me/password`,
    {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      credentials: 'include',
      body: JSON.stringify(password),
    }
  )

  if (response.status === 400) {
    const message = await response.text()

    throw new Error(
      message || 'Could not change password.'
    )
  }

  if (!response.ok) {
    throw new Error('Could not change password.')
  }
}

export async function logout() {
  await fetch(`${API_URL}/auth/logout`, {
    method: 'POST',
    credentials: 'include',
  })
}