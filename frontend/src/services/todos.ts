

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


export async function getTodos(): Promise<Todo[]> {
  const response = await fetch(`${API_URL}/todos`, {
    credentials: 'include',
  })

  if (!response.ok) {
    throw new Error('Failed to fetch todos.')
  }

  return response.json()
}
