import { useEffect, useState } from 'react'
import { getTodos, type Todo } from '../services/todos'
import KanbanColumn from '../components/KanbanColumn'

function DashboardPage() {
  const [todos, setTodos] = useState<Todo[]>([])
  const [isLoading, setIsLoading] = useState(true)
  const [error, setError] = useState('')

  useEffect(() => {
    getTodos()
      .then((data) => {
        setTodos(data)
      })
      .catch((error) => {
        console.error(error)
        setError('Could not load your todos.')
      })
      .finally(() => {
        setIsLoading(false)
      })
  }, [])

  const todoTasks = todos.filter(
    (todo) => todo.status === 'Todo'
  )

  const inProgressTasks = todos.filter(
    (todo) => todo.status === 'InProgress'
  )

  const doneTasks = todos.filter(
    (todo) => todo.status === 'Done'
  )

  if (isLoading) {
    return <p>Loading todos...</p>
  }

  if (error) {
    return <p>{error}</p>
  }

  return (
    <main className="kanban-page">
      <div className="kanban-page-header">
        <div>
          <h1>My Todos</h1>
          <p>Manage your tasks and track your progress.</p>
        </div>

        <button className="create-todo-button">
          + New Todo
        </button>
      </div>

      <div className="kanban-board">
        <KanbanColumn
          title="To Do"
          todos={todoTasks}
        />

        <KanbanColumn
          title="In Progress"
          todos={inProgressTasks}
        />

        <KanbanColumn
          title="Done"
          todos={doneTasks}
        />
      </div>
    </main>
  )
}

export default DashboardPage