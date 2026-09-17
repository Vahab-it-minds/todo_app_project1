

const API_URL = 'http://localhost:5000'

export type TodoStatus =
  | 'Todo'
  | 'InProgress'
  | 'Done'

export type Priority =
  | 'Low'
  | 'Medium'
  | 'High'

export type Todo = {
  id: number
  title: string
  description: string | null
  createdAt: string
  updatedAt: string
  dueDate: string | null
  userId: number
  priority: Priority
  timeEstimate: number | null
  category: string | null
  status: TodoStatus
}

export type CreateTodoRequest = {
  title: string
  description: string | null
  dueDate: string | null
  priority: Priority
  timeEstimate: number | null
  category: string | null
}

export type UpdateTodoRequest = {
  title: string
  description: string | null
  dueDate: string | null
  priority: Priority
  timeEstimate: number | null
  category: string | null
  status: TodoStatus
}


export async function getTodos(): Promise<Todo[]> {
  const response = await fetch(`${API_URL}/todos`, {
    credentials: 'include',
  })

  if (!response.ok) {
    throw new Error('Failed to fetch todos.')
  }

  return response.json()
}


export async function createTodo(
  todo: CreateTodoRequest
): Promise<Todo> {
  const response = await fetch(`${API_URL}/todos`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    credentials: 'include',
    body: JSON.stringify(todo),
  })

  if (!response.ok) {
    throw new Error('Failed to create todo.')
  }

  return response.json()
}


export async function updateTodo(
  id: number,
  todo: UpdateTodoRequest
): Promise<Todo> {
  const response = await fetch(`${API_URL}/todos/${id}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
    },
    credentials: 'include',
    body: JSON.stringify(todo),
  })

  if (!response.ok) {
    throw new Error('Failed to update todo.')
  }

  return response.json()
}

export async function deleteTodo(id: number): Promise<void> {
  const response = await fetch(`${API_URL}/todos/${id}`, {
    method: 'DELETE',
    credentials: 'include',
  })

  if (!response.ok) {
    throw new Error('Failed to delete todo.')
  }
}